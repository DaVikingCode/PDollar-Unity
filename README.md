$P Point-Cloud Recognizer
=========================
[Original article](http://depts.washington.edu/aimgroup/proj/dollar/pdollar.html). [Unity Web demo](http://aymericlamboley.fr/blog/wp-content/uploads/2014/07/index.html).

This is an adaptation of the original C# code for Unity.

In the demo, only one point-cloud template is loaded for each of the 16 gesture types. You can add additional templates as you wish, and even define your own custom gesture templates.

![](http://aymericlamboley.fr/blog/wp-content/uploads/2014/07/multistrokes.gif)

About
-----
The [$P](http://depts.washington.edu/aimgroup/proj/dollar/pdollar.html) Point-Cloud Recognizer is a 2-D gesture recognizer designed for rapid prototyping of gesture-based user interfaces. In machine learning terms, $P is an instance-based nearest-neighbor classifier with a Euclidean scoring function, i.e., a geometric template matcher. $P is the latest in the dollar family of recognizers that includes [$1](http://depts.washington.edu/aimgroup/proj/dollar/index.html) for unistrokes and [$N](http://depts.washington.edu/aimgroup/proj/dollar/ndollar.html) for multistrokes. Although about half of $P's code is from $1, unlike both $1 and $N, $P does not represent gestures as ordered series of points (i.e., strokes), but as unordered point-clouds. By representing gestures as point-clouds, $P can handle both unistrokes and multistrokes equivalently and without the combinatoric overhead of $N. When comparing two point-clouds, $P solves the classic [assignment problem](http://en.wikipedia.org/wiki/Assignment_problem) between two bipartite graphs using an approximation of the [Hungarian algorithm](http://en.wikipedia.org/wiki/Hungarian_algorithm). The $P recognizer is distributed under the [New BSD License](http://en.wikipedia.org/wiki/BSD_licenses#3-clause_license_.28.22Revised_BSD_License.22.2C_.22New_BSD_License.22.2C_or_.22Modified_BSD_License.22.29) agreement.

License
-----
BSD-3
