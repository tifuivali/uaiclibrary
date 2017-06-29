import { Component, OnInit } from '@angular/core';
import { ErorHelper } from '../../../helpers/error.helper';
import { Notifier } from '../../notifications/notifier';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Faculty } from '../../../models/faculty';
import { FacultyService } from '../../../services/faculty.service';

@Component({
    selector: 'faculty-administration',
    templateUrl: './faculty-administration.component.html'
})

export class FacultyAdministrationComponent implements OnInit {
    constructor(
        private errorHelper: ErorHelper,
        private notifier: Notifier,
        private route: ActivatedRoute,
        private router: Router,
        private facultyService: FacultyService
    ) {
    }

    private model: Faculty = new Faculty(0, null, null);

    initFacultyData() {
        this.facultyService.getFacultyById(this.model.id)
            .subscribe(faculty => {
                this.model = faculty;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            })
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            var facultyId = params['facultyId'];
            if(facultyId){
                if(facultyId > 0) {
                    this.model.id = facultyId;
                    this.initFacultyData();
                }
            }
        }, err => {
            this.notifier.notifyError("AnErrorOcurred");
        });
    }

    onSubmit() {
        this.facultyService.saveFaculty(this.model)
            .subscribe(faculty => {
                this.model = faculty;
                this.notifier.notifySuccess('ModificationWasSaved');
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }
    
    removeFaculty() {
        if(this.model.id <= 0) return;
        this.facultyService.removeFaculty(this.model.id)
            .subscribe(ok => {
                this.notifier.notifySuccess("FacultyWasDeleted");
                this.router.navigate(['/faculties']);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            })
    }

}