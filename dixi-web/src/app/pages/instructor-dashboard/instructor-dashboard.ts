import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-instructor-dashboard',
  standalone: true,
  templateUrl: './instructor-dashboard.html',
  styleUrl: './instructor-dashboard.scss',
})
export class InstructorDashboard {
  private route = inject(ActivatedRoute);
  userId = this.route.snapshot.paramMap.get('id');
}