<div>

<div>

# [Advent of Code](/) {#advent-of-code .title-global}

-   [\[About\]](/2021/about)
-   [\[Events\]](/2021/events)
-   [\[Shop\]](https://teespring.com/stores/advent-of-code)
-   [\[Log In\]](/2021/auth/login)

</div>

<div>

#    [var y=]{.title-event-wrap}[2021](/2021)[;]{.title-event-wrap} {#var-y2021 .title-event}

-   [\[Calendar\]](/2021)
-   [\[AoC++\]](/2021/support)
-   [\[Sponsors\]](/2021/sponsors)
-   [\[Leaderboard\]](/2021/leaderboard)
-   [\[Stats\]](/2021/stats)

</div>

</div>

::: {#sidebar}
::: {#sponsor}
::: quiet
Our [sponsors](/2021/sponsors) help make Advent of Code possible:
:::

::: sponsor
[CodingNomads](https://codingnomads.co) - Get Trained. Get Support. Get
Hired -\> 596f75 20526f636b21 :)
:::
:::
:::

::: {role="main"}
## \-\-- Day 9: Smoke Basin \-\--

These caves seem to be [lava
tubes](https://en.wikipedia.org/wiki/Lava_tube). Parts are even still
volcanically active; small hydrothermal vents release smoke into the
caves that slowly [settles like
rain]{title="This was originally going to be a puzzle about watersheds, but we're already under water."}.

If you can model how the smoke flows through the caves, you might be
able to avoid it and be that much safer. The submarine generates a
heightmap of the floor of the nearby caves for you (your puzzle input).

Smoke flows to the lowest point of the area it\'s in. For example,
consider the following heightmap:

    2199943210
    3987894921
    9856789892
    8767896789
    9899965678

Each number corresponds to the height of a particular location, where
`9` is the highest and `0` is the lowest a location can be.

Your first goal is to find the *low points* - the locations that are
lower than any of its adjacent locations. Most locations have four
adjacent locations (up, down, left, and right); locations on the edge or
corner of the map have three or two adjacent locations, respectively.
(Diagonal locations do not count as adjacent.)

In the above example, there are *four* low points, all highlighted: two
are in the first row (a `1` and a `0`), one is in the third row (a `5`),
and one is in the bottom row (also a `5`). All other locations on the
heightmap have some lower adjacent location, and so are not low points.

The *risk level* of a low point is *1 plus its height*. In the above
example, the risk levels of the low points are `2`, `1`, `6`, and `6`.
The sum of the risk levels of all low points in the heightmap is
therefore `15`.

Find all of the low points on your heightmap. *What is the sum of the
risk levels of all low points on your heightmap?*

To play, please identify yourself via one of these services:

[\[GitHub\]](/auth/github) [\[Google\]](/auth/google)
[\[Twitter\]](/auth/twitter) [\[Reddit\]](/auth/reddit) [- [\[How Does
Auth Work?\]](/about#faq_auth)]{.quiet}
:::
