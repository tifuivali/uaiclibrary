import { BrowserModule } from '@angular/platform-browser';
import { JsonpModule } from '@angular/http'
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './components/app/app.component'
import { AppRoutingModule } from './router/app-routing-module';
import { AppDeclarationsModule } from './app-declarations-module'
import { AppProvidersModule } from './app-providers-module';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { SocketIoModule, SocketIoConfig } from 'ng2-socket-io';
import { config } from './constants/socket.config';
import { FroalaEditorModule } from 'ng2-froala-editor/ng2-froala-editor';
import { Ng2DatetimePickerModule } from 'ng2-datetime-picker';

@NgModule({
  declarations: AppDeclarationsModule,
  imports: [
    BrowserModule,
    JsonpModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    FroalaEditorModule,
    Ng2DatetimePickerModule,
    SimpleNotificationsModule.forRoot(),
    SocketIoModule.forRoot(config),
  ],
  providers: AppProvidersModule,
  bootstrap: [AppComponent]
})
export class AppModule { }
