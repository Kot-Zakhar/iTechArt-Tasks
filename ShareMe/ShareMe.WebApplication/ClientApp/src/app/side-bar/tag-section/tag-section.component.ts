import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tag-section',
  templateUrl: './tag-section.component.html',
  styleUrls: ['./tag-section.component.css']
})
export class TagSectionComponent implements OnInit {
  public tags: Tag[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject('API_URL') apiUrl) {
    http.get<Tag[]>(baseUrl + apiUrl + 'tags?count=10').subscribe(result => {
      this.tags = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
