import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostModel } from '../models/post-model';

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
  })
  export class PostComponent {
    public post: PostModel;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<PostModel>(baseUrl + 'Posts/1').subscribe(result => {
          this.post = result;
        }, error => console.error(error));
      }
  }