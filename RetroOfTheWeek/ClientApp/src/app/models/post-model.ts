import { UserModel } from "./user-model";

export interface PostModel {
    id: number;
    title: string;
    text: string;
    posterId: string;
    date: Date;
    tags: string;
    img: string;
    thumbs: string;
    emailAuthor: number;
    hidden: number;
    views: number;
    rating: number;
    poster: UserModel;
}
