// ScratchpadC.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <cstdio>
#include <stdlib.h>  
#include <string.h>  
#include <Windows.h>
#include "../../Interface/MeshUnmanaged/MeshUnmanaged.h"


using DecryptFn = int(__stdcall *) (
	const char* InputArray, int InputLength,
	char**OutputArray, int *OutputLength);

int main() {

	const char* Module = "C:\\Users\\hallam\\Work\\mmm\\Interface\\MeshMananaged\\bin\\x86\\Release\\MeshMananaged.dll";

	HMODULE mod = LoadLibraryA(Module);
	printf("Module %s\n", Module);
	printf("Module %p\n", mod);

	DecryptFn Decrypt = reinterpret_cast<DecryptFn>(GetProcAddress(mod, "Decrypt"));
	printf("Decrypt %p\n", Decrypt);

	Mesh_Initialize(Module);

	const char* test = "This is a test";
	for (int i = 0; i < 10; i++) {
		char *result=NULL;
		int length=0;

		Decrypt(test, strlen(test), &result, &length);

		printf("Result %s\n", result);

		free(result);
		}



	return 0;
	}

