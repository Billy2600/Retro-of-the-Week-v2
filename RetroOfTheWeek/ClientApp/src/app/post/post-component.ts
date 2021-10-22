import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from "@angular/router";
import { PostModel } from '../models/post-model';

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
  })
  export class PostComponent {
    public post: PostModel;
    private id: number;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
        this.route.params.subscribe( params => this.id = params.id );
        http.get<PostModel>(baseUrl + 'Posts/' + this.id).subscribe(result => {
          this.post = result;
        }, error => console.error(error));
      }
  }