import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
  })
  export class PostComponent {
    public post: Post;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<Post>(baseUrl + 'Posts/1').subscribe(result => {
          this.post = result;
        }, error => console.error(error));
      }
  }

interface Post {
    pid: number;
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
    name: number;
    email: string;
}