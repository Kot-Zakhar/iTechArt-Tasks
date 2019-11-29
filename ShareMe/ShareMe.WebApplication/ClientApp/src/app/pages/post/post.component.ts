import { Component, Inject, OnInit } from '@angular/core';
import { Post } from 'src/lib/models/entities/Post';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
})
export class PostComponent implements OnInit {
  public post: Post;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('API_URL') private apiUrl: string
  ) {}

  ngOnInit() {
    const postId = this.route.snapshot.paramMap.get('id') ;
    this.http.get<Post>(this.baseUrl + this.apiUrl + 'posts/' + postId).subscribe(result => {
      this.post = result;
    }, error => console.error(error));
  }
}
