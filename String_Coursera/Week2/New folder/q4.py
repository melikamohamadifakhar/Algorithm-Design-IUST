# python3
import sys

def suffixarray(text):
    suffixes = []

    for i in range(len(text)):
        suffixes.append((i, text[i:]))

    suffixes = sorted(suffixes, key=lambda t: t[1])

    result = []
    for i in range(len(suffixes)):
        result.append(suffixes[i][0])

    return result


if __name__ == '__main__':
    text = sys.stdin.readline().strip()
    print(" ".join(map(str, suffixarray(text))))