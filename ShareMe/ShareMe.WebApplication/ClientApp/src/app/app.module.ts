import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PostPreviewComponent } from './postPreview/postPreview.component';
import { CategoriesSectionComponent } from './side-bar/categories-section/categories-section.component';
import { PopularPostsComponent } from './side-bar/popular-posts/popular-posts.component';
import { SocialPluginComponent } from './side-bar/social-plugin/social-plugin.component';
import { TagSectionComponent } from './side-bar/tag-section/tag-section.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PostPreviewComponent,
    CategoriesSectionComponent,
    PopularPostsComponent,
    SocialPluginComponent,
    TagSectionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'postPreview', component: PostPreviewComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
