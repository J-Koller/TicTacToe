import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { PlayerDto } from "../data/dto/player-dto";
import { Subscription } from "rxjs";
import { GameService } from "../services/game-service";

@Component({
  selector: "app-game-board",
  templateUrl: "./game-board.component.html",
  styleUrls: ["./game-board.component.css"],
})
export class GameBoardComponent implements OnChanges, OnInit {

  @Input() opponentPlayerDto: PlayerDto | null = null;
  opponentInfo: string = "";
  turnInfo: string = "";

  buttons: Array<any> = Array.from({ length: 9 }, (_, index) => ({
    id: index,
    disabled: false,
  }));

  constructor(private gameService: GameService){}

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes["opponentPlayerDto"]) {
      this.opponentInfo = `Opponent: ${this.opponentPlayerDto?.username} | Rank: ${this.opponentPlayerDto?.rank}`;
    }
  }

  disableButtons(): void {
    this.buttons.forEach((button) => (button.disabled = true));
  }

  enableButtons(): void {
    this.buttons.forEach((button) => (button.disabled = false));
  }
}
