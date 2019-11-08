import { Component, Inject, OnInit } from '@angular/core';
import { Post } from 'src/lib/models/Post';
import { HttpClient } from '@angular/common/http';

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
    this.http.get<Post[]>(this.baseUrl + this.apiUrl + 'posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }
}
