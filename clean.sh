#!/bin/bash
echo "Pre-clean step"
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +
