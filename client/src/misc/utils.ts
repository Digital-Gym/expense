export function randomColor(){
  const r = Math.floor(Math.random() * 255);
  const g = Math.floor(Math.random() * 255);
  const b = Math.floor(Math.random() * 255);

  return `rgb(${r},${g},${b})`;
}

export function formatCurrency(value: number | undefined){
  if(value){
    return value.toLocaleString('en-US').replace(/,/g, ' ');
  }
  return 0;
}
