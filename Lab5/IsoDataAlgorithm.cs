using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    class IsoDataAlgorithm
    {
        int numberOfClusters;
        public IList<Cluster> RunAlgorithm(IList<Cluster> clusters, IList<Point> points, int k, double qN, double qS, double qC, int l, int iterations)
        {
            Random rnd = new Random();
            numberOfClusters = clusters.Count;
            for (int i = 1; i <= iterations; i++)
            {
                var splitClusters = SplitIntoClusters(clusters, points);
                clusters = RemoveRedundantClusters(splitClusters, qN).ToList();
                LocalizeClustersCenter(clusters);
                var averageDistances = FindAverageDistances(clusters);
                var generalizedAverageDistance = FindGeneralizedAverageDistance(averageDistances, clusters);

                if (i == iterations)
                {
                    //qC = 0;
                    var distances = FindDistanceBetweenClasterCenters(clusters);
                    var ranked = PerformRanking(qC, distances, l).Select(r => (r.Item1, r.Item2));
                    Cluster newCluster;
                    foreach (var clstr in ranked)
                    {
                        newCluster = MergeClusters(clstr.Item1, clstr.Item2);
                        clusters.Remove(clstr.Item1);
                        clusters.Remove(clstr.Item2);
                        clusters.Add(newCluster);
                    }
                    continue;
                }

                if (numberOfClusters <= k / 2)
                {
                    var standartDeviations = FindVectorOfStandartDeviation(clusters).ToList();
                    var averagesDistancesAsList = averageDistances.ToList();
                    var clusterList = new List<Cluster>(clusters);
                    for (int j = 0; j < standartDeviations.Count; j++)
                    {
                        if(CheckSplitConditions(standartDeviations[j], qS, averagesDistancesAsList[j], generalizedAverageDistance, clusterList[j].Points.Count, numberOfClusters, k, qN))
                        {
                            var previousCenter = clusterList[j].Center;
                            clusterList.Remove(clusterList[j]);
                            var gamma = rnd.NextDouble();
                            if (gamma == 0)
                                gamma = 1;
                            clusterList.Add(new Cluster(new Point(previousCenter.X + gamma, previousCenter.Y + gamma)));
                            clusterList.Add(new Cluster(new Point(previousCenter.X - gamma, previousCenter.Y - gamma)));
                        }
                    }
                    clusters = new List<Cluster>(clusterList);
                }

                if (i % 2 == 0 && numberOfClusters >= 2 * k)
                {
                    var distances = FindDistanceBetweenClasterCenters(clusters);
                    var ranked = PerformRanking(qC, distances, l).Select(r => (r.Item1, r.Item2));
                    Cluster newCluster;
                    foreach (var clstr in ranked)
                    {
                        newCluster = MergeClusters(clstr.Item1, clstr.Item2);
                        clusters.Remove(clstr.Item1);
                        clusters.Remove(clstr.Item2);
                        clusters.Add(newCluster);
                    }
                }
                else { }
            }
            var result = SplitIntoClusters(clusters, points);
            return result.ToList();
        }

        IEnumerable<Cluster> SplitIntoClusters(IList<Cluster> clusters, IEnumerable<Point> images)
        {
            ClearClusters(clusters);
            if (clusters.Count() == 1)
            {
                clusters.FirstOrDefault().Points = images.ToList();
            }
            else
            {
                List<double> distances = new List<double>(clusters.Count());
                foreach (var image in images)
                {
                    foreach (var cluster in clusters)
                    {
                        var center = cluster.Center;
                        distances.Add(Math.Sqrt(Math.Pow((image.X - center.X), 2) + Math.Pow((image.Y - center.Y), 2)));
                    }
                    var min = distances.Min();
                    var minIndex = distances.IndexOf(min);
                    if (!clusters[minIndex].Points.Contains(image))
                    {
                        clusters[minIndex].Points.Add(image);
                    }
                    distances.Clear();
                }
            }
            return clusters;
        }

        IEnumerable<Cluster> RemoveRedundantClusters(IEnumerable<Cluster> clusters, double qN)
        {
            var result = clusters.Where(c => c.Points.Count >= qN);
            numberOfClusters = result.Count();
            return result;
        }

        void LocalizeClustersCenter(IEnumerable<Cluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                var clusterPoints = cluster.Points;
                cluster.Center = new Point(clusterPoints.Average(p => p.X), clusterPoints.Average(p => p.Y));
            }
        }

        IList<double> FindAverageDistances(IEnumerable<Cluster> clusters)
        {
            var result = clusters.Select(c =>
            c.Points.Select(p =>
            Math.Sqrt(Math.Pow(p.X - c.Center.X, 2) + Math.Pow(p.Y - c.Center.Y, 2))))
                .Select(c => Math.Round(c.Average(), 3));

            return result.ToList();
        }

        double FindGeneralizedAverageDistance(IEnumerable<double> averageDistances, IEnumerable<Cluster> clusters)
        {
            double result = 0;
            var averageDistancesAsList = averageDistances.ToList();
            var clustersAsList = clusters.ToList();
            for (int i = 0; i < clusters.Count(); i++)
            {
                result += clustersAsList[i].Points.Count * averageDistancesAsList[i];
            }
            return result / clusters.Sum(c => c.Points.Count);
        }

        IEnumerable<double> FindVectorOfStandartDeviation(IEnumerable<Cluster> clusters)
        {
            var clustersAsList = clusters.ToList();
            List<(double, double)> result = new List<(double, double)>();
            for (int i = 0; i < clustersAsList.Count; i++)
            {
                var points = clustersAsList[i].Points;
                double helperX = 0, helperY = 0;
                for (int j = 0; j < points.Count; j++)
                {
                    helperX += Math.Pow(points[j].X - clustersAsList[i].Center.X, 2);
                    helperY += Math.Pow(points[j].Y - clustersAsList[i].Center.Y, 2);
                }
                result.Add((Math.Sqrt(helperX / points.Count), (Math.Sqrt(helperY / points.Count))));
            }
            return result.Select(r => r.Item1 >= r.Item2 ? r.Item1 : r.Item2);
        }

        bool CheckSplitConditions(double standartDeviation, double qS, double averageDistance, double generalizedAverageDistance, int numberOfPoints, int numberOfClasters, int k, double qN)
        {
            return standartDeviation > qS && averageDistance > generalizedAverageDistance && (numberOfPoints > 2 * (qN + 1) || numberOfClasters <= k / 2);
        }

        IList<(Cluster, Cluster, double)> FindDistanceBetweenClasterCenters(IList<Cluster> clusters)
        {
            List<(Cluster, Cluster, double)> result = new List<(Cluster, Cluster, double)>();
            for (int i = 0; i < clusters.Count - 1; i++)
            {
                for (int j = i + 1; j < clusters.Count; j++)
                {
                    result.Add((clusters[i], clusters[j], Math.Sqrt(Math.Pow((clusters[i].Center.X - clusters[j].Center.X), 2) + Math.Pow((clusters[i].Center.Y - clusters[j].Center.Y), 2))));
                }
            }
            return result;
        }

        IEnumerable<(Cluster, Cluster, double)> PerformRanking(double qC, IList<(Cluster, Cluster, double)> distances, int l)
        {
            return distances.Where(d => d.Item3 < qC).OrderBy(d => d).Take(l);
        }

        Cluster MergeClusters(Cluster cluster1, Cluster cluster2)
        {
            var newX = 1 / (cluster1.Points.Count + cluster2.Points.Count) * (cluster1.Center.X * cluster1.Points.Count + cluster2.Center.X * cluster2.Points.Count);
            var newY = 1 / (cluster1.Points.Count + cluster2.Points.Count) * (cluster1.Center.Y * cluster1.Points.Count + cluster2.Center.Y * cluster2.Points.Count);
            return new Cluster(new Point(newX, newY));
        }

        void ClearClusters(IEnumerable<Cluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                cluster.Points.Clear();
            }
        }

    }
}
