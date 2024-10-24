import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tela-inicial',
  templateUrl: './tela-inicial.component.html',
  styleUrls: ['./tela-inicial.component.css']
})
export class TelaInicialComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {

    let currentSlide = 0;

    function showSlide(index: number) {
        const slides = document.querySelectorAll('.banner');
        if (index >= slides.length) currentSlide = 0;
        if (index < 0) currentSlide = slides.length - 1;

        slides.forEach((slide, i) => {
            slide.classList.remove('active');
            if (i === currentSlide) {
                slide.classList.add('active');
            }
        });
    }

    function moveSlide(direction: number) {
        currentSlide += direction;
        showSlide(currentSlide);
    }

    // Iniciar o carrossel mostrando o primeiro banner
    showSlide(currentSlide);
  }
}
