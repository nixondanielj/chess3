$(document).ready(function () {
    board = $('.gameboard');
    drawBoard();
});
var board, darkSquare;

function drawBoard() {
    board.children(".boardrow").each(function (elI, el) {
        var row = $(el);
        for (var i = 0; i < 8; i++) {
            addSquareToRow(elI % 2 == 0, row);
        }
    });
}

function addSquareToRow(evenRow, row) {
    var square = $('<div>0</div>')
        .addClass(darkSquare ^ evenRow ? "dark" : "light")
        .addClass("square");
    darkSquare = !darkSquare;
    row.append(square);
}