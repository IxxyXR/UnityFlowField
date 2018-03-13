# UnityFlowField
Vector flow fields for Unity particles

Still a work in progress.

Supports:

1. hardcoded equations to map position to velocity (see the case/switch statement in the main script)
2. Image maps with that take each particles x/y position, sample an RGB pixel from the source image and then uses RGB to control xyx velocity.

To do:

1. A GPU only implementation - maybe using https://github.com/kamindustries/Dust
2. More controls
3. Runtime eval for equations
4. A better preset system in general
5. More friendly parametric ways to generate fields

Inspired by https://anvaka.github.io/fieldplay . See https://www.reddit.com/r/fieldplay/top/ for some ideas for equations


This uses submodules so please clone with: "git clone --recursive (url)" and update with "git submodule update --init"
