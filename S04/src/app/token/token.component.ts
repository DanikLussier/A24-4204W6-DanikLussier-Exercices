import { Component } from '@angular/core';
import { SpotifyService } from '../../services/spotify.service';
import { Artist } from '../../models/artist';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Token } from '@angular/compiler';
import { HttpClient, HttpHeaderResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-token',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './token.component.html',
  styleUrl: './token.component.css'
})

export class TokenComponent {

  artistName : string = "";
  artist ?: Artist;

  constructor(public spotify : SpotifyService, public http : HttpClient) {
    
  }

  ngOnInit() {
    this.spotify.connect()
  }

  async getArtist() : Promise<void>{
    this.artistName = (await this.spotify.searchArtist(this.artistName)).name
    this.artist = await this.spotify.searchArtist(this.artistName)
  }

}
