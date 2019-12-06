import { Entity } from '../entities/Entity';

export class GridModel<EntityT extends Entity> {
    values: EntityT[];
    pageIndex: number;
    pageSize: number;
    next: boolean;
    previous: boolean;
    url: string;
    nextPageUrl: string;
    previousPageUrl: string;
}
