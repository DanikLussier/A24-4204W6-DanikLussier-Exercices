import { Item } from './../models/Item';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { iterator } from 'rxjs/internal/symbol/iterator';

const domain = "https://localhost:7034";

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(public http: HttpClient) { }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //    GetAll
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async getAll(): Promise<Item[]> {

    var items: Item[] = new Array<Item>
    await lastValueFrom(this.http.get<Item[]>(domain + "/api/Items/GetItem")).then((val) => {
      items = val
    }).catch((err) => {
      console.log("Service error while http request")
    })
    console.log(items)

    return items
  }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //      Get
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async get(id: number): Promise<Item | null> {

    var item: Item | null = null
    await lastValueFrom(this.http.get<Item>(domain + "/api/Items/GetItem/" + id)).then((val) => {
      item = val
      console.log(item)
    }).catch((err) => { console.log("Erreur en cherchant l'item précis") })

    if (item != null)
      return item
    else
      return null

  }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //     Post
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async post(name: string, value: number): Promise<void> {

    var item: Item = new Item(0, name, value)
    let x = await lastValueFrom(this.http.post<Item>(domain + "/api/Items/PostItem", item))

    console.log(x)

  }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //    Delete
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async delete(id: number): Promise<void> {

  }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //     Put
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async put(id: number, name: string, value: number): Promise<void> {

    var item: Item = new Item(id, name, value)
    await lastValueFrom(this.http.put<Item>(domain + "/api/Items/PutItem/" + id, item)).then((val) => {
      console.log("Update de l'item réussi")
    }).catch((err) => {
      console.log("Erreur en changeant l'item : " + err)
    })

  }

  // ▄▄▄▄▄▄▄▄▄▄▄▄
  //     Nuke
  // ▀▀▀▀▀▀▀▀▀▀▀▀
  async nuke(): Promise<void> {



  }

}
