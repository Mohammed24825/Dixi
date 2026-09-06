import { Component, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-loading',
  standalone: true,
  templateUrl: './loading.html',
  styleUrl: './loading.scss',
})
export class LoadingComponent implements OnInit {
  progress = signal(0);

  constructor(private router: Router) {}

  ngOnInit() {
  console.log('loading mounted, progress:', this.progress());
  const durationMs = 5000;
  const stepMs = 50;
  const steps = durationMs / stepMs;
  let current = 0;

  const interval = setInterval(() => {
    current++;
    this.progress.set(Math.min(100, (current / steps) * 100));
    console.log('tick', current, this.progress());
    if (current >= steps) {
      clearInterval(interval);
      this.router.navigate(['/home']);
    }
  }, stepMs);
}
}