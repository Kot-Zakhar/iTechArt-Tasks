import { Entity } from './Entity';

export class Category extends Entity {
    name: string;
    parentCategory?: Category;
}
