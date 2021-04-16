#include <omp.h>
#include <sys/time.h>


void randomiseMatrix(int **matrix, int n, int m) {
	for(int i = 0; i < n; i++) {
		for(int j = 0; j < m; j++) {
			matrix[i][j] = rand() % 11;
		}
	}
	return;
}

void startThread(int **result,int **matrix1,int **matrix2, int m1,int m2, int n1,int countT){
		int threadsNum = countT;
		struct timeval begin, end;
		omp_set_dynamic(0);
		omp_set_num_threads(threadsNum);
		int i, j, k;
		gettimeofday(&begin, NULL);
		#pragma omp parallel for shared(matrix1, matrix2, result) private(i, j, k)
		for (i = 0; i < n1; i++) {
			for (j = 0; j < m2; j++) {
				result[i][j] = 0;
				for (k = 0; k < m1; k++) {
					result[i][j] += (matrix1[i][k] * matrix2[k][j]);
				}
			}
		}
		gettimeofday(&end, NULL);
		printf("%14u | %10ld \n",threadsNum,(end.tv_sec-begin.tv_sec));
	//	printf("number of threads : %u | size matrix [%u;%u] | %ld micro seconds \n",threadsNum,((end.tv_sec * 1000000 + end.tv_usec)-(begin.tv_sec * 1000000 + begin.tv_usec)));
}

int main(int argc, char** argv) {

	int size[3] = {1000, 2000, 3000};
	printf("%14s | %10s \n","Count Thread","Time");
	for(int indexS = 0; indexS < 3; indexS++){
		int n1 = size[indexS];
		int m1 = size[indexS];
		int n2 = size[indexS];
		int m2 = size[indexS];
		printf("******* %4u x %4u *******\n", n1,m1);
		//Матрица n1 x m1
		int **matrix1;
		//Матрица n2 x m2
		int **matrix2;
		matrix1 = (int**)malloc(sizeof(int*)*n1);
		for(int i = 0; i < n1; i++) {
			matrix1[i] = (int*)malloc(sizeof(int)*m1);
		}
		matrix2 = (int**)malloc(sizeof(int*)*n2);
		for(int i = 0; i < n2; i++) {
			matrix2[i] = (int*)malloc(sizeof(int)*m2);
		}
		//Генерируем случайные матрицы для умножения
		randomiseMatrix(matrix1, n1, m1);
		randomiseMatrix(matrix2, n2, m2);
		int **result = (int**)malloc(sizeof(int*)*n1);;
		for(int i = 0; i < n1; i++) {
			result[i] = (int*)malloc(sizeof(int)*m2);
		}
		for(int i=1;i<=8;i++)
			startThread(result, matrix1, matrix2, m1, m2, n1, i);
	}
	return 0;
}
