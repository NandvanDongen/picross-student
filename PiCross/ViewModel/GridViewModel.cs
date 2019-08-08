using Cells;
using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Utility;

namespace ViewModel
{
    public class GridViewModel
    {
        public ISequence<IPlayablePuzzleConstraints> ColumnConstraints { get; }
        public ISequence<IPlayablePuzzleConstraints> RowConstraints { get; }
        public IGrid<SquareViewModel> Griding { get; }
        public Puzzel P { get; }
        public IPlayablePuzzle Playable { get; }
        public TijdMeter TijdMeter { get; }
         

        public GridViewModel(Puzzel puzzel)
        {
            this.P = puzzel;
            var facade = new PiCrossFacade();
           this.TijdMeter = new TijdMeter(Playable);
            if (P == null)
            {
                var puzzle = Puzzle.FromRowStrings(
                    "xxxxx",
                    "x...x",
                    "x...x",
                    "x...x",
                    "xxxxx"
                );

                var playablePuzzle = facade.CreatePlayablePuzzle(puzzle);
                Playable = playablePuzzle;
                Griding = Grid.Create(puzzle.Size, p => new SquareViewModel(playablePuzzle.Grid[p], Playable));
                RowConstraints = playablePuzzle.RowConstraints;
                ColumnConstraints = playablePuzzle.ColumnConstraints;
                TijdMeter.start();
                TijdMeter.tick();
            }
            else
            {
                /*voor elke square in playablepuzzle wordt een squareviewmodel aangemaakt daarmee wordt een grid aangemaakt*/
                var playablePuzzle = facade.CreatePlayablePuzzle(puzzel.Puzzle);
                Playable = playablePuzzle;
                Griding = DataStructures.Grid.Create(puzzel.Puzzle.Size, p => new SquareViewModel(playablePuzzle.Grid[p], Playable));
                TijdMeter.start();
                TijdMeter.tick();
                while (TijdMeter.started)
                {
                    TijdMeter.CurrentTime();
                }

                /*column en rowconstraints uitlezen van de playable puzzle en meegeven als property*/
                RowConstraints = playablePuzzle.RowConstraints;
                ColumnConstraints = playablePuzzle.ColumnConstraints;
            }
        }
    }

    public class SquareViewModel
    {
        public IPlayablePuzzle playablePuzzle { get; }

        public IPlayablePuzzleSquare PuzzleSquare { get; }

        public ICommand Click { get; set; }

        public Cell<Square> Contents { get; }

        /* "wrapper class voor square"*/
        public SquareViewModel(IPlayablePuzzleSquare square, IPlayablePuzzle puzzle)
        {
            playablePuzzle = puzzle;
            PuzzleSquare = square;
            Contents = PuzzleSquare.Contents;
            Click = new ClickHandler(this);

        }

        /*klik klasse die invulling van square bepaalt/veranderd*/
        private class ClickHandler : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private SquareViewModel s;
            private IPlayablePuzzle playingPuzzle;

            public ClickHandler(SquareViewModel s)
            {
                this.s = s;
                this.playingPuzzle = s.playablePuzzle;
            }

            public bool CanExecute(object parameter)
            {
                if (playingPuzzle.IsSolved.Value)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public void Execute(object parameter)
            {
                if (s.PuzzleSquare.Contents.Value == Square.FILLED)
                {
                    s.Contents.Value = Square.EMPTY;
                }

                else if (s.PuzzleSquare.Contents.Value == Square.EMPTY)
                {
                    s.Contents.Value = Square.UNKNOWN;
                }

                else if (s.PuzzleSquare.Contents.Value == Square.UNKNOWN)
                {
                    s.Contents.Value = Square.FILLED;
                }
            }
        }
    }

    public class SquareConverter : IValueConverter
    {
        public object Filled { get; set; }

        public object Empty { get; set; }

        public object Unknown { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var square = (Square)value;
            if (square == Square.EMPTY)
            {
                return Empty;
            }
            else if (square == Square.FILLED)
            {
                return Filled;
            }
            else
            {
                return Unknown;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SatisfiedConverter : IValueConverter
    {
        /*propertes*/
        public object IsSatisfied { get; set; }

        public object IsNotSatisfied { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*cast to boolean to check if IsSatisfied see playablePuzzle*/
            var isSatisfied = (Boolean)value;
            if (isSatisfied)
            {
                return IsSatisfied;
            }
            else
            {
                return IsNotSatisfied;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsSolvedConvertor : IValueConverter
    {
        public object IsSolved { get; set; }
        public object IsNotSolved { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var solved = (Boolean)value;
            if (solved)
            {
                return IsSolved;
            }
            else
            {
                return IsNotSolved;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PuzzlesViewModel
    {
        public List<Puzzel> Puzzels { get; }

        public PuzzlesViewModel()
        {
            Puzzels = PuzzleBuild.getPuzzles();
        }
    }

    public class TijdMeter : INotifyPropertyChanged
    {
        public Chronometer chrono { get; }
        public IPlayablePuzzle puzzle;
        public Boolean started = false;
        private TimeSpan _totalTime;
        public event PropertyChangedEventHandler PropertyChanged;

        public TijdMeter(IPlayablePuzzle playable)
        {
            chrono = new Chronometer();
            _totalTime = chrono.TotalTime.Value;
            puzzle = playable;
        }

        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set
            {
                if(_totalTime != value)
                {
                    _totalTime = value;
                    OnPropertyChanged("TotalTime");
                }
            }
        }

        public void start()
        {
            chrono.Start();
        }

        public void tick()
        {
            chrono.Tick();
        }

        public void pauze()
        {
            if (puzzle.IsSolved.Value)
            {
                chrono.Pause();
            }
        }

        public void CurrentTime()
        {
            while (chrono.started)
            {
                TotalTime = chrono.TotalTime.Value;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}

