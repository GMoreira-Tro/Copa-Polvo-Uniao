import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tela-inicial',
  templateUrl: './tela-inicial.component.html',
  styleUrls: ['./tela-inicial.component.css']
})
export class TelaInicialComponent implements OnInit {
  currentSlide: number = 0;
  slides: string[] = [
    '../assets/images/Emblema.webp',
    '../assets/images/Emblema.webp',
    '../assets/images/Emblema.webp'
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.showSlide(this.currentSlide);
  }

  showSlide(index: number): void {
    const slides = document.querySelectorAll('.banner');
    if (index >= slides.length) this.currentSlide = 0;
    if (index < 0) this.currentSlide = slides.length - 1;

    slides.forEach((slide, i) => {
      slide.classList.remove('active');
      if (i === this.currentSlide) {
        slide.classList.add('active');
      }
    });
  }

  moveSlide(direction: number): void {
    this.currentSlide += direction;
    this.showSlide(this.currentSlide);
  }
}
