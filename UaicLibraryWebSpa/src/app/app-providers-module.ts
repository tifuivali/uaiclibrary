import { TRANSLATION_PROVIDERS, TranslatePipe, TranslateService } from './translations';
import { LanguageListComponent } from "./components/navmenu/language-list.component"
import { StringHelper } from "./helpers/string.helper"
import { CookieService } from 'angular2-cookie/services/cookies.service';
import { ApiUrlHelper } from './http-request/http-request';
import { UserService } from './services/user.service';
import { HttpClient } from './http-client';
import { Notifier } from './components/notifications/notifier';
import { ErorHelper } from './helpers/error.helper';
import { ModalService } from './services/modal-service';
import { AppCookieHelper } from './helpers/app-cookie.helper';
import { AuthService } from './services/auth.service';
import { StudentService } from './services/student.service';
import { FacultyService } from './services/faculty.service';
import { BookService } from './services/book.service';
import { AuthorService } from './services/author.service';
import { ArrayHelper } from './helpers/array.helper';
import { BookCommentService } from './services/book.comment.service';
import { AdministrationService } from './services/administration.service';

export const AppProvidersModule = [
    HttpClient,
    TRANSLATION_PROVIDERS,
    TranslateService,
    StringHelper,
    CookieService,
    ApiUrlHelper,
    UserService,
    Notifier,
    ErorHelper,
    ModalService,
    AppCookieHelper,
    AuthService,
    StudentService,
    FacultyService,
    BookService,
    AuthorService,
    ArrayHelper,
    BookCommentService,
    AdministrationService
] 