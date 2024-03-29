import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from 'src/lib/models/entities/Category';

@Component({
  selector: 'app-categories-section',
  templateUrl: './categories-section.component.html',
  styleUrls: ['./categories-section.component.css']
})
export class CategoriesSectionComponent implements OnInit {
  public categories: Category[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject('API_URL') apiUrl) {
    http.get<Category[]>(baseUrl + apiUrl + 'categories?count=4').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
