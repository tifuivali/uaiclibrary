import { Routes } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { FetchDataComponent } from '../components/fetchdata/fetchdata.component';
import { CounterComponent } from '../components/counter/counter.component';
import { SignUp } from '../components/authentification/signup/signup.component';
import { PageNotFound } from '../components/error/page-not-found.component';
import { Login } from '../components/authentification/login/login.component';
import { StudentProfileComponent } from '../components/user/profile/student-profile.component';
import { BookPageComponent } from '../components/bookPage/book-page.component';
import { BookViewerComponent } from '../components/bookViewer/book-viewer.component';
import { AdministrationPageComponent } from '../components/bookAdministration/administration-page.component';
import { LatestBooksComponent } from '../components/latestBooks/latestBooks.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'signup', component: SignUp },
  { path: 'login', component: Login },
  { path: 'studentaccount', component: StudentProfileComponent },
  { path: 'books', component: BookPageComponent },
  { path: 'viewBook/:bookId/:page/:viewAnnotation', component: BookViewerComponent },
  { path: 'administration', component: AdministrationPageComponent },
  { path: 'editBook/:bookId', component: AdministrationPageComponent },
  { path: 'editAuthor/:authorId', component: AdministrationPageComponent },
  { path: 'editFaculty/:facultyId', component: AdministrationPageComponent },
  { path: 'latestBooks', component: LatestBooksComponent },
  { path: '**', component: PageNotFound }
];