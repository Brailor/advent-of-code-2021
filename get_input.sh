# /usr/bin/env bash

DAY="$1"

if [ -z "$SESSION" ]; then
    echo "Session value is empty. Maybe you forgot to set it in an environment value?"
    echo "eg: export SESSION=<SESSION_TOKEN>"

    exit 1
fi

if [ -z "$DAY" ]; then
    echo "Looks like you forgot to add the day paramater."
    echo "Usage: "
    echo "./get_input.sh <DAY>"

    exit 1
fi

if [ ! -d "day-$DAY" ]; then
    echo "Solution directory for day: $DAY not found."
    while read -rp "Would you like to create it? [y/n]" answer; do
        if [ "$answer" = "n" ]; then
            echo "You choose not to proceed. Exiting script."
            exit 1
        fi

        if [[ "$answer" =~ y|Y ]]; then
            echo "Creating directory..."
            mkdir -p "day-$DAY"
            break
        fi
    done
fi

echo "Getting input source file:"
curl -b session="$SESSION" "https://adventofcode.com/2021/day/$DAY/input" -o "day-$DAY/input.txt"

echo "Fetching problem description..."
pandoc -f html -t markdown "https://adventofcode.com/2021/day/$DAY" -o "day-$DAY/problem.md"

exit 0
