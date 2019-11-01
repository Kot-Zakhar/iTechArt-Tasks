import { User } from './User';
import { Entity } from './Entity';
import { Category } from './Category';

export class Post extends Entity {
    uri: string;
    title: string;
    author: User;
    creationDate: Date;
    rating: number;
    content: Text;
    imageSrc: string;
    category: Category;
    tags: Tag[];
}
