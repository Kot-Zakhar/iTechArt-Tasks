import { Component, Input } from '@angular/core';
import { Post } from 'src/lib/models/entities/Post';

@Component({
  selector: 'app-post-preview',
  templateUrl: './postPreview.component.html',
})
export class PostPreviewComponent {
    @Input() post: Post;
}
