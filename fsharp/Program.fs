// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp


open one
open two
open System



[<EntryPoint>]
let main argv =
    let input2 = two.readLines "two/input.txt"
    let problem1 = two.problem1 input2
    let problem2 = two.problem2 input2


    printf "day - #2: \n problem - #1: %A \n problem - #2: %A \n" problem1 problem2

    0 // return an integer exit code