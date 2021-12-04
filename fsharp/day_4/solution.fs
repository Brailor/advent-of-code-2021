module day_4
open System.IO
open System.Text.RegularExpressions

type Marked = bool
type Point = int * Marked
type Board = list<list<Point>>


let input_seq = File.ReadAllLines "day_4/input.txt" |> Array.filter (fun s -> s.Length <> 0)
let input_boards = (input_seq |> Array.tail |> Array.chunkBySize 5)
let input2 = [17;58;52;49;72;33;55;73;27;69;88;80;9;7;59;98;63;42;84;37;87;28;97;66;79;77;61;48;83;5;94;26;70;12;51;82;99;45;22;64;10;78;13;18;15;39;8;30;68;65;40;21;6;86;90;29;60;4;38;3;43;93;44;50;41;96;20;62;19;91;23;36;47;92;76;31;67;11;0;56;95;85;35;16;2;14;75;53;1;57;81;46;71;54;24;74;89;32;25;34]

let r = input_boards |> Array.map (fun row ->
    row |> Array.map (fun s -> 
            Regex.Split(s , @"\s+") |> Array.filter (fun v -> v.Length <> 0) |>Array.map(fun v -> 
                int v
            )

    )
)

type makeBoard = array<array<int>> -> Board
let makeBoard: makeBoard = fun list ->
    let r = list |> Array.toList |> List.map (fun rows ->
        let new_rows = rows |> Array.toList |> List.map (fun elem -> 
            (elem, false)
        )

        new_rows
    )

    r

let sequence = [59; 98; 74; 18; 35; 62; 84; 16; 0;]

let board: Board = [
    [(59, false); (98, false); (84, false);];
    [(17, false); (35, false); (18, false);];
    [(62, false); (16, false); (74, false);];
]
let BOARD_LENGTH = 5
let check (board: Board) (elem: int) =
    let row_mark_count (list: list<Point>) = 
                (0, list) ||> List.fold (fun i (_, marked) ->
                if marked then
                    i + 1
                else 
                    i
            )
    let do_check (row, col) =
        let col_mark_count = ( 0, board) ||> List.fold (fun acc board_col ->
                let (_, mark) = board_col.[col]
                if mark then
                     acc + 1
                else
                     acc
            )

        let row_mark_count = row_mark_count board.[row]
    
        match (row_mark_count, col_mark_count) with
        | (a, b) -> (a >= BOARD_LENGTH || b >= BOARD_LENGTH)
        
        
    let mutable isWinningBoard = false
    board |> List.iteri (fun i col ->
            col |> List.iteri (fun j (row_elem, _) ->
            if row_elem = elem then
                isWinningBoard <- do_check (i, j)
        )
    )

    isWinningBoard

let mark (board: Board) (elem: int) =
    board |>List.map (fun col ->
        col |> List.map (fun (row_elem, row_mark) ->
            if row_elem = elem then
                (row_elem, true)
            else 
                (row_elem, row_mark)
        )
    )


let isWinningBoard (elem: int) (board: Board) =
    let marked_board = mark board elem
    let isWinningBoard = check marked_board elem

    (marked_board, isWinningBoard)


let problem1 = 
    let mutable boards = r |> Array.map (fun b -> makeBoard b) |> Array.toList
    input2 |> List.iter (fun next_num ->
        let new_boards = boards |> List.map (fun board ->
            mark board next_num
        )

        let ne = new_boards |> List.filter (fun a ->
            not(check a next_num)
        )

        printfn "filtered boards: %A, elem: %A" ne next_num
        
        boards <- ne
    )