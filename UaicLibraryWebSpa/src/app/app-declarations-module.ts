import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';;
import { TranslatePipe } from './translations';
import { LanguageListComponent } from "./components/navmenu/language-list.component"
import { Footer } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { SignUp } from './components/authentification/signup/signup.component';
import { AppRoutingModule } from './router/app-routing-module';
import { PageNotFound } from './components/error/page-not-found.component';
import { Login } from './components/authentification/login/login.component';
import { RadioGrup } from './components/common/radioGrup/radio-group.component';
import { ValidationErrors } from './components/error/validationErors/validation-errors.component';
import { StudentProfileComponent } from './components/user/profile/student-profile.component';
import { ProfileHeaderComponent } from './components/user/profile/profileHeader/profile-header.component';
import { FileSelectDirective } from 'ng2-file-upload';
import { ModalComponent } from './components/common/modal/modal.component';
import { SingleFileUploader } from './components/common/fileUpload/single-file-upload.component';
import { MultiSelect } from './components/common/multiselect/multi-select.component';
import { BookComponent } from './components/book/book.component';
import { BookPageComponent } from './components/bookPage/book-page.component';
import { BookListComponent } from './components/bookList/book-list.component';
import { PaginationComponent } from './components/common/pagination/pagination.component';
import { DropdownComponent } from './components/common/dropdown/dropdown.component';
import { BookCommentListComponent } from './components/bookCommentList/book-comment-list.component';
import { CommentComponent } from './components/comment/comment.component';
import { NewCommentComponent } from './components/common/newComment/new-comment.component';
import { BookViewerComponent } from './components/bookViewer/book-viewer.component';
import { UserModalListComponent } from './components/user/modalUserList/modal-user-list.component';
import { AnnotateModalComponent } from './components/common/editor/annotate-modal/annotate-modal.component';
import { AdministrationPageComponent } from './components/bookAdministration/administration-page.component';
import { BookAdministrationComponent } from './components/bookAdministration/bookAdministration/book-administration.component';
import { AuthorAdministrationComponent } from './components/bookAdministration/authorAdministration/author-administration.component';
import { FacultyAdministrationComponent } from './components/bookAdministration/facultyAdministration/faculty-administration.component';
import { LatestBooksComponent } from './components/latestBooks/latestBooks.component';
import { HomeContentAdministrationComponent } from './components/bookAdministration/homeContentAdministration/homeContent-administration.component';

export const AppDeclarationsModule = [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    SignUp,
    LanguageListComponent,
    TranslatePipe,
    Footer,
    PageNotFound,
    Login,
    RadioGrup,
    ValidationErrors,
    StudentProfileComponent,
    ProfileHeaderComponent,
    FileSelectDirective,
    ModalComponent,
    SingleFileUploader,
    MultiSelect,
    BookComponent,
    BookPageComponent,
    BookListComponent,
    PaginationComponent,
    DropdownComponent,
    CommentComponent,
    BookCommentListComponent,
    NewCommentComponent,
    BookViewerComponent,
    UserModalListComponent,
    AnnotateModalComponent,
    AdministrationPageComponent,
    BookAdministrationComponent,
    AuthorAdministrationComponent,
    FacultyAdministrationComponent,
    LatestBooksComponent,
    HomeContentAdministrationComponent
];