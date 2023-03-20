module day_12

open System.IO

// start-A
// start-b
// A-c
// A-b
// b-d
// A-end
// b-end

// A node can contain children nodes
// A node will have a visited property, for certain nodes it means the node can only be visited exactly 1 time
// for other nodes there is no such constraint. (uppercase nodes ['A' in this example])

// start is the root node which have 2 children nodes: [A, b]
// end is the end node which will be pointed by [A, b]

type CaveSize =
    | Small
    | Big

type Cave = { Name: string; Size: CaveSize }

let create_cave (str: string) =
    let size =
        if str.ToUpper() = str then
            Big
        else
            Small

    { Name = str; Size = size }

let input =
    let dict = Map.empty<Cave, array<Cave>>

    (dict, File.ReadAllLines "day_12/example.txt")
    ||> Array.fold (fun map line ->
        let parts = line.Split "-"

        match parts with
        | [| a; b |] ->
            let left = create_cave a
            let right = create_cave b
            if map |> Map.containsKey left then
                map
                |> Map.change left (fun v ->
                    match v with
                    | Some children -> Some(children |> Array.append [|right|])
                    | None -> None)
            else
                map |> Map.add left [|right|]
        | _ -> failwith "error during parsing")
// printfn "input: %A" input

let cave_connections =
    File.ReadAllLines "day_12/example.txt"
    |> Array.map (fun str ->
        match str.Split('-') with
        | [| l; r |] ->
            let left = create_cave l
            let right = create_cave r
            [| (left, right); (right, left) |]
        | _ -> [||])
    |> Array.concat
    // |> Array.filter (fun (l,r) -> l.Name <> "end" && r <> start)
    |> Array.filter (fun (left, right) -> left.Name <> "end" && right.Name <> "start")
    |> Array.groupBy fst
    |> Array.map (fun (k, v) -> (k, v |> Array.map snd))
    |> Map.ofArray


let can_visit (cave: Cave) (visited: list<Cave>) =
    match cave.Size with
    | Small -> not (visited |> List.contains cave)
    | Big -> true

let can_visit_2 (cave: Cave) (visited: list<Cave>) =
    let small_cave_visit =
        visited
        |> List.filter (fun c -> c.Size = Small)
        |> List.filter (fun c -> not (c.Name = "start") && not (c.Name = "end"))
        |> List.countBy (fun c -> c.Name)
        |> List.map snd
        |> List.append [ 0 ]
        |> List.max

    match cave.Size with
    | Small ->
        (not (visited |> List.contains cave))
        || small_cave_visit < 2
    | Big -> true

let problem =
    let solve can_visit =
        let rec walk (cave: Cave) (visited: list<Cave>) =
            if cave.Name = "end" then
                [ cave :: visited ]
            else
                let connections = input |> Map.tryFind cave

                match connections with
                | None -> []
                | Some connections ->
                    printfn "connections for cave: %A are: %A" cave connections
                    connections 
                    |> Array.map (fun next_cave ->
                    let new_visited = cave :: visited

                    if new_visited |> can_visit next_cave then
                        walk next_cave new_visited
                    else
                        [ new_visited ])
                    |> List.concat

        let start_cave = create_cave "start"
        let end_cave = create_cave "end"

        walk start_cave []
        // |> List.filter (fun res -> res |> List.contains end_cave)
        // |> List.distinct
        |> List.length

    let p1 = solve can_visit
    // let p2 = solve can_visit_2

    printfn "part 1: %A" p1
    // printfn "part 2: %A" p2
    0



