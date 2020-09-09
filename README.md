# Lab3D misc

This repository contains most of the works concerning the scientific initiation project at the Laboratório de Realidade Virtual da COPPE in between January 2020 and March 2020.

The files inside this repository run on Unity 2019.3 and use the Vuforia plug-in for augmented reality functionality.

There are two different applications inside this repository:

1. an interactive periodic table, in which each real-life marker represents a major group on the periodic table of elements. When the user points a camera at the marker, it should appear elements on the screen, hovering above the marker for as long as the camera is able to maintain the marker on frame. The user then is able to tap the elements on the screen to see a visual representation of the elements.

2. a molecule builder in which every possible marker is an atom of an element. Whenever a camera captures the image of a marker, the application shows the visual representation of the atom (just like in the interactive periodic table), and whenever there are two markers physically close enough from each other, if the atoms they represent are able to bond, then a chemical bond is formed betweent them two. While, in real life, the bondings happen because of valence (and other factors that doesn't matter to the context of the application), the bondings in this project are stored in a dictionary structure.

There are a couple of things that are still to be done within this project:
 - represent different types of chemical bond between atoms
 - optimize bonding so every possible molecule isn't just a prefab waiting to be loaded (which isn't that big of a problem, once the application was originally intended for a specific environment)
 
 Rio de Janeiro, 2020
