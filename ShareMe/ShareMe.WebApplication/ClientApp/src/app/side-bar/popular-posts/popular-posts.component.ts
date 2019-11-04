import { Component, OnInit, Inject } from '@angular/core';
import { Post } from 'src/lib/models/Post';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-popular-posts',
  templateUrl: './popular-posts.component.html',
  styleUrls: ['./popular-posts.component.css']
})
export class PopularPostsComponent implements OnInit {
  public popularPosts: Post[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject('API_URL') apiUrl: string) {
    http.get<Post[]>(baseUrl + apiUrl + 'posts?count=3').subscribe(result => {
      this.popularPosts = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
