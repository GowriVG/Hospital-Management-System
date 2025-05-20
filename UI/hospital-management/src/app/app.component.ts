import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
//import { NavbarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { PatientListComponent } from './components/patient/patient-list/patient-list.component';    
//import { DasboardComponent } from './dasboard/dasboard.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'hospital-management';
}

