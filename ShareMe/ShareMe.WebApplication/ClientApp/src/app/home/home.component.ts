import { Component, Inject } from '@angular/core';
import { Post } from 'src/lib/models/Post';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public posts: Post[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject('API_URL') apiUrl: string) {
    http.get<Post[]>(baseUrl + apiUrl + 'posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }
}
