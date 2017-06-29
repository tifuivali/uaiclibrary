import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'administration-page',
    templateUrl: './administration-page.component.html'
})

export class AdministrationPageComponent implements OnInit {

    private tabNumber: number;

    constructor(private route: ActivatedRoute) {
        this.tabNumber = 1;
    }

    changeTab(tabNumber: number) {
        this.tabNumber = tabNumber;
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            var authorId = params['authorId'];
            var facultyId = params['facultyId'];
            if(authorId)
            {
                if(authorId > 0)
                {
                    this.changeTab(2);
                }
            }
            if(facultyId)
            {
                if(facultyId > 0)
                {
                    this.changeTab(3);
                }
            }
        });
    }



}