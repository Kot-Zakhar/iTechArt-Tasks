import { Component, OnInit, Inject } from '@angular/core';
import { Post } from 'src/lib/models/entities/Post';
import { HttpClient } from '@angular/common/http';
import { GridModel } from 'src/lib/models/grid/GridModel';

@Component({
  selector: 'app-popular-posts',
  templateUrl: './popular-posts.component.html',
  styleUrls: ['./popular-posts.component.css']
})
export class PopularPostsComponent implements OnInit {
  public popularPosts: Post[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject('API_URL') apiUrl: string) {
    http.get<GridModel<Post>>(baseUrl + apiUrl + 'posts?pageSize=3&sortField=rating&sortType=desc').subscribe(result => {
      this.popularPosts = result.values;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
