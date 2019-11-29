import { Component, Inject, OnInit } from '@angular/core';
import { Post } from 'src/lib/models/entities/Post';
import { HttpClient } from '@angular/common/http';
import { GridModel } from 'src/lib/models/grid/GridModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public posts: Post[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('API_URL') private apiUrl: string
  ) {}

  ngOnInit() {
    this.http.get<GridModel<Post>>(this.baseUrl + this.apiUrl + 'posts').subscribe(result => {
      this.posts = result.values;
    }, error => console.error(error));
  }
}
