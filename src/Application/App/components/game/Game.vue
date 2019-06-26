<template>
    <div id="gameboard" class="w-100 h-100 mb-3">
        <div class="d-block text-center w-100 my-3">
            <h2 class="text-white">{{gameStatusTitle}}</h2>
            <h3 v-if="gameObject.isFinished" class="text-white">{{gameWinningStatus}}</h3>
            <button @click="initGame" v-if="gameObject.isFinished" class="btn btn-main">RESET GAME</button>
        </div>
        <div class="d-flex w-100">
            <table id="gameboard-table">
                <tr class="gb-table-row m-0 p-0" v-for="row in 3">
                    <td class="gb-table-column m-0 p-0" v-for="column in 3">
                        <button @click="clickCell(row, column)" :disabled="isCellClicked(row, column)" :class="{'gb-clicked-btn' : isCellClicked(row, column) == true, 'gb-winner-btn': isCellWinning(row, column) == true }" class="gb-action-btn d-block">{{getSymbolPerMove(row, column)}}</button>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</template>
<script>
export default {
  data () {
        return {
            gameObject: null,
            gameStatusTitle: '',
            gameWinningStatus: '',
            moves: [],
            winningCombination: null
       }
  },
        methods: {
            isCellWinning: function (row, column) {
                let move = this.getMovePerCoordinates(row, column);
                return (move != null &&
                    this.gameObject.isFinished &&
                    this.gameObject.isPlayerWin != null &&
                    this.winningCombination.includes(move.position));
            },
            clickCell: function (row, column) {
                let self = this;
                this.$http.post("/api/game/make-move/" + this.gameObject.id, {
                        'position': column + (3*(row - 1))
                    })
                    .then(response => {
                        self.gameObject = response.data;
                        self.updateGameStatusString();
                        self.getAllMoves();
                        self.updateGameWinningStatusString();
                        self.parseWinningCombination();
                    })
                    .catch();
            },
            isCellClicked: function (row, column) {
                let move = this.getMovePerCoordinates(row, column);

                return move != null;
            },
            getSymbolPerMove: function (row, column) {
                let move = this.getMovePerCoordinates(row, column);
                let resultSymbol = '';
                if (move != null) {
                    resultSymbol = move.isPlayer == true ? this.gameObject.playerSign : this.gameObject.machineSign;
                }

                return resultSymbol;
            },
            getMovePerCoordinates: function (row, column) {
                for (var i = 0; i < this.moves.length; i++) {
                    if (this.moves[i].row == row && this.moves[i].column == column) {
                        return this.moves[i];
                    }
                }
                return null;
            },
            updateGameStatusString: function () {
                if (this.gameObject != null) {
                    if (this.gameObject.isFinished) {
                        this.gameStatusTitle = 'Current Game Is Finished';
                    }
                    else {
                        this.gameStatusTitle = 'Current Game Is Not Finished';
                    }
                }
            },
            updateGameWinningStatusString: function () {
                if (this.gameObject != null) {
                    if (this.gameObject.isPlayerWin == true) {
                        this.gameWinningStatus = 'Winner is Player';
                    }
                    else if (this.gameObject.isPlayerWin == false) {
                        this.gameWinningStatus = 'Winner is Machine';
                    }
                    else {
                        this.gameWinningStatus = 'Game is Draw';
                    }
                }
            },
            initGame: function () {
                let self = this;
                this.$http.post("/api/game/init")
                    .then(response =>
                    {
                        self.gameObject = response.data;
                        self.updateGameStatusString();
                        self.getAllMoves();
                    })
                    .catch();
            },
            getAllMoves: function () {
                let self = this;
                this.$http.get('/api/game/get-all-moves-per-game/' + this.gameObject.id)
                    .then(response => {
                        self.moves = response.data;
                    })
                    .catch();
            },
            parseWinningCombination: function () {
                if (this.gameObject.winningCombination != null) {
                    let winningCombinationString = this.gameObject.winningCombination.toString();
                    this.winningCombination = [
                        parseInt(winningCombinationString[0]),
                        parseInt(winningCombinationString[1]),
                        parseInt(winningCombinationString[2])
                    ];  
                }
            }
  },
  mounted: function () {
      this.initGame();
  }
}
</script>
<style>
    #gameboard > h2 {
        width: 100% !important;
    }
    #gameboard-table {
        width: auto;
        margin: auto;
        border: 3px solid #44b684;
    }
        #gameboard-table .gb-action-btn {
            width: 100px;
            height: 100px;
            border: 3px solid #44b684;
            box-shadow: none;
            background: #1f2f48;
            color: #fff;
            font-weight: bold;
            font-size: 40px;
            text-align: center;
        }

    .gb-clicked-btn {
        background: #317d5c !important;
    }
    .gb-winner-btn {
        background: #ce665c !important;
    }
</style>