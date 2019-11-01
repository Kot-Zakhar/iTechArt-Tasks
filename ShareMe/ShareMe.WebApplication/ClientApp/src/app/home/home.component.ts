import { Component } from '@angular/core';
import { Post } from 'src/lib/models/Post';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  posts: Post[] = [
    {
       author: {
         email: 'tempEmail',
         id: 'id',
         name: 'vasya',
         surname: 'petrov',
         username: 'Vasya.Petrov'
       },
       category: {
         id: 'id',
         name: 'categoryName',
       },
       content: new Text('Here is some content'),
       creationDate: new Date(2000, 12, 10, 14, 0),
       id: 'postId',
       imageSrc: '../../assets/TempPic.jpg',
       rating: 100,
       title: 'Post title',
       uri: 'TempPosturi'
    }
  ];

}
