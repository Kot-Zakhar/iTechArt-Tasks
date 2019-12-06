import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { PostPreviewComponent } from './components/postPreview/postPreview.component';
import { PostComponent } from './pages/post/post.component';
import { CategoriesSectionComponent } from './components/side-bar/categories-section/categories-section.component';
import { PopularPostsComponent } from './components/side-bar/popular-posts/popular-posts.component';
import { TagSectionComponent } from './components/side-bar/tag-section/tag-section.component';
import { environment } from '../environments/environment';
import { from } from 'rxjs';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PostPreviewComponent,
    PostComponent,
    CategoriesSectionComponent,
    PopularPostsComponent,
    TagSectionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'post/:id', component: PostComponent },
    ],
    {
      // enableTracing: !environment.production
    }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
