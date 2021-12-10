module day_10
open System.IO

let example = File.ReadAllLines "day_10/input.txt" |> Array.map (Seq.toArray)

let opening_chars = [|'['; '('; '{'; '<'|]
let closing_chars = [|']'; ')'; '}'; '>'|]

let is_opening_char (char: char) =
    opening_chars |> Array.contains char

let is_pair (opening: char, closing: char) =
    let char_open_idx = opening_chars |> Array.findIndex (fun x -> x = opening)
    let closing_char = char_open_idx |> Array.get closing_chars

    closing = closing_char
    
let problem1 = 
    let rec parser (stack: array<char>) (arr: array<char>) =
        if Array.length arr = 0 then
            if Array.length stack <> 0 then
                stack
            else
                [||]
        else
            let char = arr |> Array.head
            match is_opening_char char with
            | true -> parser (stack |> Array.append [|char|]) (arr |> Array.tail)
            | false ->
                let last_opening = stack |> Array.head
                match is_pair (last_opening, char) with
                | true -> 
                    parser (stack |> Array.tail) (arr |> Array.tail)
                | false ->
                    [|char|]

    let res = example |> Array.map (fun line -> parser [||] line) |> Array.filter (fun a -> a |> Array.length <> 0) |> Array.concat
    printfn "part 1: %A" ((0,res) ||> Array.fold(fun acc item ->
        match item with
        | ')' -> acc + 3
        | ']' -> acc + 57
        | '}' -> acc + 1197
        | '>' -> acc + 25137
        | _ -> acc
    ))
    0

let problem2 =
    let get_closing_part (opening_char: char) =
        let char_open_idx = opening_chars |> Array.findIndex (fun x -> x = opening_char)

        char_open_idx |> Array.get closing_chars

    let problem2 (onpenings_to_match: array<char>) =
        onpenings_to_match |> Array.map get_closing_part
    let rec parser (stack: array<char>) (arr: array<char>) =
        if Array.length arr = 0 then
            if Array.length stack <> 0 then
                stack
            else
                [||]
        else
            let char = arr |> Array.head
            match is_opening_char char with
            | true -> parser (stack |> Array.append [|char|]) (arr |> Array.tail)
            | false ->
                let last_opening = stack |> Array.head
                match is_pair (last_opening, char) with
                | true -> 
                    parser (stack |> Array.tail) (arr |> Array.tail)
                | false -> [||]
    let res = example |> Array.map(fun line -> parser [||] line) |> Array.filter (fun line -> line |> Array.length <> 0 )
    let closings = 
        res 
        |> Array.map problem2 
        |> Array.map (fun line -> (uint64 0, line) ||> Array.fold(fun acc char ->
            match char with
            | ')' -> acc * uint64 5 + uint64 1
            | ']' -> acc * uint64 5 + uint64 2
            | '}' -> acc * uint64 5 + uint64 3
            | '>' -> acc * uint64 5 + uint64 4
            | _ -> acc
        ))
        |> Array.sort
    let middle = (closings |> Array.length) / 2

    printfn "part 2: %A" (middle |> Array.get closings)
    0