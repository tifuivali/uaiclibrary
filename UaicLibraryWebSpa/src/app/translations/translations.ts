import { OpaqueToken } from '@angular/core';
import { langDictionary } from './translations-dictionary'

export const TRANSLATIONS = new OpaqueToken('translations'); 




export const TRANSLATION_PROVIDERS = [
    { provide: TRANSLATIONS, useValue: langDictionary },
]; 