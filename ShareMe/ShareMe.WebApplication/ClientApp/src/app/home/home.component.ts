import { Component, Inject } from '@angular/core';
import { Post } from 'src/lib/models/Post';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  posts: Post[];
  
  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Post[]>(apiUrl + 'posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }
}
