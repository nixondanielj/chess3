$(document).ready(function () {
    board = $('.gameboard');
    drawBoard();
});
var board, darkSquare;
var PIECES = {
    WHITEPAWN: "w",
    WHITEROOK: "wr",
    WHITEKNIGHT: "wn",
    WHITEBISHOP: "wb",
    WHITEKING: "wk",
    WHITEQUEEN: "wq",
    BLACKPAWN: "b",
    BLACKROOK: "br",
    BLACKKNIGHT: "bn",
    BLACKBISHOP: "bb",
    BLACKKING: "bk",
    BLACKQUEEN: "bq"
};

function pieceToClass(piece) {
    switch (piece) {
        case PIECES.WHITEPAWN: return "wPawn";
        case PIECES.WHITEROOK: return "wRook";
        case PIECES.WHITEKNIGHT: return "wKnight";
        case PIECES.WHITEBISHOP: return "wBishop";
        case PIECES.WHITEKING: return "wKing";
        case PIECES.WHITEQUEEN: return "wQueen";
        case PIECES.BLACKPAWN: return "bPawn";
        case PIECES.BLACKROOK: return "bRook";
        case PIECES.BLACKKNIGHT: return "bKnight";
        case PIECES.BLACKBISHOP: return "bBishop";
        case PIECES.BLACKKING: return "bKing";
        case PIECES.BLACKQUEEN: return "bQueen";
    }
    return null;
}

function drawBoard() {
    var data = getBoardData();
    board.children(".boardrow").each(function (elI, el) {
        var row = $(el);
        for (var i = 0; i < 8; i++) {
            addSquareToRow(elI % 2 == 0, row, data.Squares[(elI * 8) + i]);
        }
    });
}

function addSquareToRow(evenRow, row, piece) {
    var square = $('<div></div>')
        .addClass(darkSquare ^ evenRow ? "dark" : "light")
        .addClass("square");
    darkSquare = !darkSquare;
    var pieceClass = pieceToClass(piece);
    row.append(square);
    if (pieceClass) {
        var piece = $("<div></div>")
                        .addClass("piece")
                        .addClass(pieceClass)
                        .appendTo(square);
        centerPiece(piece, square);
    }
}

function centerPiece(piece, square) {
    piece.css("margin-left", (square.width() - piece.width()) / 2);
    piece.css("margin-top", (square.height() - piece.height()) / 2);
}

function getBoardData() {
    var data = {};
    data.Squares =
        [
            PIECES.WHITEROOK,
            PIECES.WHITEKNIGHT,
            PIECES.WHITEBISHOP,
            PIECES.WHITEKING,
            PIECES.WHITEQUEEN,
            PIECES.WHITEBISHOP,
            PIECES.WHITEKNIGHT,
            PIECES.WHITEROOK
        ];
    for (var i = 0; i < 8; i++) {
        data.Squares.push(PIECES.WHITEPAWN);
    }
    var black =
        [
            PIECES.BLACKROOK,
            PIECES.BLACKKNIGHT,
            PIECES.BLACKBISHOP,
            PIECES.BLACKKING,
            PIECES.BLACKQUEEN,
            PIECES.BLACKBISHOP,
            PIECES.BLACKKNIGHT,
            PIECES.BLACKROOK
        ];
    for (var i = 0; i < 8; i++) {
        black.push(PIECES.BLACKPAWN);
    }
    for (var i = 0; i < 32; i++) {
        data.Squares.push("");
    }
    data.Squares = data.Squares.concat(black.reverse());
    return data;
}