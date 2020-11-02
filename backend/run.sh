#!/bin/sh
tmux \
    new-session -d\; \
    split-window -h 'cd app && yarn && yarn dev --host 0.0.0.0'\; \
    split-window -v 'yarn && yarn start'\; \
    select-pane -t 0\; \
    attach
