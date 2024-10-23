import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ItemService } from './services/item.service';
import { CommonModule } from '@angular/common';
import { Item } from './models/Item';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  itemId: number | null = null;
  itemName: string | null = null;
  itemValue: number | null = null;

  item: Item | null = null;
  items: Item[] | null = null;

  constructor(public itemService: ItemService) { }

  async seeAllItems(): Promise<void> {

    this.itemService.getAll().then((val) => {
      this.items = val
    }).catch((err) => {
      console.log("Erreur en recherchant la liste de tous les items")
    })

    // Décommenter ceci et le laisser à la fin de la fonction
    this.item = null;
  }

  async seeOneItem(): Promise<void> {
    // Garder ceci en début de fonction
    if (this.itemId == null) return;

    this.itemService.get(this.itemId).then((val) => {
      this.item = val
    }).catch((err) => {
      console.log("Error : " + err)
    })

    // Décommenter ceci et le laisser à la fin de la fonction
    this.items = null;
  }

  async createItem(): Promise<void> {
    // Garder ceci en début de fonction
    if (!this.itemName || !this.itemValue) return;

    this.itemService.post(this.itemName, this.itemValue)
  }

  async editItem(): Promise<void> {
    // Garder ceci en début de fonction
    if (!this.itemId || !this.itemName || !this.itemValue) return;

    this.itemService.put(this.itemId, this.itemName, this.itemValue)

  }

  async deleteItem(): Promise<void> {
    // Garder ceci en début de fonction
    if (this.itemId == null) return;

    this.itemService.delete(this.itemId)

  }

  async deleteAllItems(): Promise<void> {
    this.itemService.nuke()

  }

}
