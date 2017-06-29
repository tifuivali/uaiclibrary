import { Component, NgModule } from '@angular/core'
import { TranslateService } from "../../../translations"
import { User } from '../../../models/autentification/user';
import { UserService } from '../../../services/user.service';
import { UserType } from '../../../constants/userType';
import { ErorHelper } from '../../../helpers/error.helper';
import { Router } from '@angular/router';
import { Notifier } from '../../notifications/notifier';
import { Error } from '../../../models/error/error';
import { FacultyService } from '../../../services/faculty.service';
import { Faculty } from '../../../models/faculty';
import { MultiSelectItem } from '../../../components/common/multiselect/multi-select-item';

@Component({
    selector: 'user-signup',
    templateUrl: './signup.component.html'
})

export class SignUp {

    constructor(private translateService: TranslateService,
        private userService: UserService,
        private facultyService: FacultyService,
        private errorHelper: ErorHelper,
        private router: Router,
        private notifier: Notifier) {
        this.model = new User(0, '1234', 'tifuivali@gmail.com', '123456', 'Tifui', 'Vali', 'Student', '123456', 3, null, []);
        this.facultyService.getAll()
            .subscribe(faculties => {
                this.allFaculties = faculties;
                var simpleModels = this.facultyService.getSimpleModelsFromFacultyModels(faculties);
                this.faculties = simpleModels;
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }



    getSelectedFacult
    allFaculties: Array<Faculty>;
    faculties: Array<MultiSelectItem>;
    model: User;
    errors: Error[];
    userTypeOptions = Object.keys(UserType);

    getFacultyById(id: number): Faculty {
        var faculty: Faculty;
        this.allFaculties.forEach(element => {
            if (element.id == id) {
                faculty = element;
                return;
            }
        });
        console.log(" ret null");
        console.log(faculty);
        return faculty;
    }

    getSelectedFaculties(): Array<Faculty> {
        var selectedFaculties: Array<Faculty> = [];
        this.faculties.forEach(element => {
            if (element.isSelected) {
                var faculty = this.getFacultyById(element.id);
                selectedFaculties.push(faculty);
            }
        });

        return selectedFaculties;
    }

    onUserTypeChange(element: any) {
        var value = element;
        this.model.Role = UserType[value];
    }

    submitted = false;

    onSubmit() {
        var selectedFaculties = this.getSelectedFaculties();
        this.model.Faculties = selectedFaculties;
        var result = this.userService.addUser(this.model);
        result.subscribe(user => {
            this.notifier.notifySuccess('SuccessRegistration');
            this.router.navigate(['/login']);
            this.submitted = true;
        },
            err => {
                console.log(err);
                if (this.hasUserAlradyError(err.errors)) {
                    this.notifier.notifyError('AccountAlreadyExists');
                    this.router.navigate(['/login']);
                    return;
                }
                this.errorHelper.handleBasicError(err.status, err.errors);
                this.submitted = false;
            });
    }

    private hasUserAlradyError(errors: Error[]): boolean {
        if (errors == undefined)
            return false;
        for (var i = 0; i < errors.length; i++) {
            if (errors[i].errorMessage === 'AccountAlreadyExists')
                return true;
        }
        return false;
    }
}

