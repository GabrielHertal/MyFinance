/*
    Em algum momento isso vai ser útil
*/
export const sleep = (ms: number) => 
    new Promise(resolve => setTimeout(resolve,ms));