py pc.py archive create -f picross.zip
find -name "*.txt" | xargs -I{} py pc.py archive add-from-solution 
picross.zip internet {}
py pc.py archive add-player picross.zip player
